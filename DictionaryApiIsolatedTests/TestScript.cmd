cd ../
cmd /C docker compose -f docker-compose-test.yml up -d --build
timeout 10
cd ./dictionaryapiisolatedtests
node mockedexternalapi/postresponse.js
cmd /C npm test
cd ../
docker compose -f docker-compose-test.yml down
cd ./dictionaryapiisolatedtests