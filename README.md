# Dad Jokes Project

## Getting Started

Clone the repo to your machine:
```shell
$ git clone https://github.com/BrandonShega/DadJokes.git
```

## Server

**Requirements**
* .NET Core

**Running Server**
```shell
$ cd DadJokesAPI/DadJokesAPI && dotnet run
```

Server should be running at `https://localhost:5001`

## Client

**Requirements**
* Node Package Manager

**Dependency Installation**
```shell
$ npm install
```

**Running Server**
```shell
$ cd dadjokesclient && npm run serve
```

Server should be running at `http://localhost:8080`

## API

The API provides two endpoints

### Random Joke

URL: `/api/jokes`

Method: `GET`

Response:
```json
{
	"id": "xHQucUvszd",
	"joke": "To the guy who invented zero... thanks for nothing."
}
```

### Search Jokes

URL: `/api/jokes/:query`

Method: `GET`

Response:
```json
{
	"short": [{
		"id": "JBs4T79Edpb",
		"joke": "Breaking news! Energizer <Bunny> arrested – charged with battery."
	}],
	"medium": [{
		"id": "PfaMusPucFd",
		"joke": "What do you do when your <bunny> gets wet? You get your hare dryer."
	}, {
		"id": "a29pbp4haFd",
		"joke": "Where do rabbits go after they get married? On a <bunny>-moon."
	}],
	"long": []
}
```
