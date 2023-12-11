using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;

namespace DictionaryApi.BusinessLayer.Services
{
	public class MeaningApiMapper : IMeaningApiMapper
	{
		private IBasicWordDetailsRepo? BasicWordDetailsRepo { get; set; }
		private IDefinitionsRepo? DefinitionsRepo { get; set; }
		private IPhoneticAudiosRepo? PhoneticAudiosRepo { get; set; }
		private IAntonymsRepo? AntonymsRepo { get; set; }
		private ISynonymsRepo? SynonymsRepo { get; set; }
		private BasicWordDetails? BasicWordDetails { get; set; }

		public MeaningApiMapper(IBasicWordDetailsRepo? basicWordDetailsRepo, BasicWordDetails? basicWordDetails,
			IPhoneticAudiosRepo? phoneticAudiosRepo, IDefinitionsRepo? definitionsRepo, IAntonymsRepo? AntonymsRepo,
			ISynonymsRepo? SynonymsRepo)
		{
			this.BasicWordDetailsRepo = basicWordDetailsRepo;
			this.BasicWordDetails = basicWordDetails;
			this.PhoneticAudiosRepo = phoneticAudiosRepo;	
			this.DefinitionsRepo = definitionsRepo;
			this.SynonymsRepo = SynonymsRepo;
			this.AntonymsRepo= AntonymsRepo;
		}

		public async Task<BasicWordDetails?> MapBasicWordDetailsAsync(IEnumerable<WordDetails> wordDetails)
		{
            BasicWordDetails.Id= Guid.NewGuid();
			BasicWordDetails.Word = wordDetails.FirstOrDefault()?.Word;
			BasicWordDetails.Origin = wordDetails.FirstOrDefault(wordDetail => wordDetail.Origin!=null && wordDetail.Origin!="")?.Origin;
			var meanings = wordDetails.Select(wordDetails => wordDetails.Meanings).SelectMany(Meaning => Meaning);
			var phonetics= wordDetails.Select(wordDetails=> wordDetails.Phonetics).SelectMany(Phonetic => Phonetic);
			await MapPhoneticAudiosAsync(phonetics);
			BasicWordDetails.DefaultDefinition = await MapDefinitionsAsync(meanings);
			await BasicWordDetailsRepo?.AddDetailsAsync(BasicWordDetails);
			return BasicWordDetails;
		}

		public async Task<string?> MapDefinitionsAsync(IEnumerable<Meaning> meanings)
		{
			var synonyms = meanings.Select(meaning => meaning.Synonyms).SelectMany(synonyms => synonyms)
				.Select(word => new Words {Id = Guid.NewGuid(), Word=word}).ToList();
			var antonyms = meanings.Select(meaning => meaning.Antonyms).SelectMany(antonyms => antonyms)
				.Select(word => new Words { Id = Guid.NewGuid(), Word = word }).ToList();
			var synonymMore = meanings.Select(meaning => meaning?.Definitions?.Select(definition => definition.Synonyms))
				.SelectMany(synonyms => synonyms).SelectMany(synonyms => synonyms).Select(word => new Words { Id = Guid.NewGuid(), Word = word });
			var antonymMore = meanings.Select(meaning => meaning?.Definitions?.Select(definition => definition.Antonyms))
				.SelectMany(antonyms => antonyms).SelectMany(antonyms=>antonyms).Select(word => new Words { Id = Guid.NewGuid(), Word = word });
			synonyms.Union(synonymMore).OrderBy(x => x.Id).ToList();
			antonyms.Union(antonymMore).OrderBy(x => x.Id).ToList();
			await AntonymsRepo.AddAntonymsAsync(new Antonyms { Id = Guid.NewGuid(), Antonym = antonyms, BasicWordDetailsId = this.BasicWordDetails.Id });
			await SynonymsRepo.AddSynonymsAsync(new Synonyms { Id = Guid.NewGuid(), Synonym = synonyms, BasicWordDetailsId = this.BasicWordDetails.Id });

			var partOfSpeeches = meanings.Select(meaning => meaning.PartOfSpeech).Select(partOfSpeech=>new PartOfSpeech
			{
				Id = Guid.NewGuid(),
				Name = partOfSpeech
			});
			var definitions = (IEnumerable<DefinitionDto>)meanings.Select(meaning => new DefinitionDto
			{
				Id = Guid.NewGuid(),
				DefinitionText = meaning.Definitions.Select(definition => definition.DefinitionText).FirstOrDefault(defText => defText!=null && defText!=""),
				Example = meaning.Definitions.Select(definition => definition.Example).FirstOrDefault(defExmp => defExmp != null && defExmp != ""),
				PartOfSpeech = partOfSpeeches.FirstOrDefault(partOfSpeech=>partOfSpeech.Name==meaning.PartOfSpeech), 
				BasicWordDetailsId = this.BasicWordDetails.Id
			});
			this.BasicWordDetails.NumberOfDefinitions = definitions.Count();
			foreach (var  definition in definitions)
			{
				await DefinitionsRepo?.AddDefinitionAsync(definition);
			}
			return definitions.FirstOrDefault(Definition => Definition.DefinitionText != null && Definition.DefinitionText != "")?.DefinitionText;
		}

		public async Task<string?> MapPhoneticAudiosAsync(IEnumerable<Phonetic> phonetics)
		{
			BasicWordDetails.DefaultPhoneticsText = phonetics.FirstOrDefault(phonetic => phonetic.PhoneticText != null && phonetic.PhoneticText != "")?.PhoneticText;
			var pronounceLink = phonetics.FirstOrDefault(phonetic => phonetic.PronounceLink != null && phonetic.PronounceLink != "")?.PronounceLink;
			if (pronounceLink != null)
			{
				this.BasicWordDetails.IsPronounceLnkPresent= true;
				var phoneticAudio = new PhoneticDto{ Id = Guid.NewGuid(), PronounceLink = pronounceLink, BasicWordDetailsId=this.BasicWordDetails.Id };
				await PhoneticAudiosRepo.AddPronounciationAsync(phoneticAudio);
			}
			return "";
		}

	}
}
