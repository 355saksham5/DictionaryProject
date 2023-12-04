using DictionaryApi.Models.UserCache;
using System.Collections.Concurrent;

namespace DictionaryApi.Extensions
{
  public static class QueueExtension
	{
		public static void EnqueueExtension<T>(this ConcurrentQueue<T> queue, T word, int Limit)
		{
			queue.Enqueue(word);
			T? overflow;
			while (queue.Count > Limit && queue.TryDequeue(out overflow)) ;
		}
	}
}
