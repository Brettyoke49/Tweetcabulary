# Tweetcabulary

This project is an ASP.NET Core web app that allows users to enter in a given twitter handle and receive metrics about their vocabulary and word usage.

Notable dependencies include TweetinviAPI and Spell.Check (aka aspnetspell).

The server will fail to start unless an "APIKeys.json" file exists within the Tweetcabulary directory. It must include three valid consumer keys, which are to be presented in this structure:

{
	"APIKey": "xxx",
	"APISecret": "xxx",
	"Bearer": "xxx"
}
