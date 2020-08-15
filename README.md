# F0-Talks-AsyncAwait
My talk on asynchronous programming with async and await in .NET (C#).

## Everything you (don't) want to know about async/await

### 2019-11-04 - .NET Community Austria
#### [Event (Meetup)](https://www.meetup.com/dotnet-austria/events/263414974/)
#### [Recording (YouTube)](https://www.youtube.com/watch?v=flGlypydA8c)
#### [Demos](./demo/) - requires .NET Core SDK >= 3.1.100
#### [Presentation](./presentation/) - created with [Try .NET](https://github.com/dotnet/try)
#### [Errata](./erratum/Errata_2019-11-04.md)
#### [References](./reference/References_2019-11-04.md)

## Run the presentation
### Manually
1. navigate to _./presentation/_
2. `dotnet tool restore` to make the [dotnet-try](https://github.com/dotnet/try/blob/master/DotNetTryLocal.md) local tool available
3. `dotnet try` to launch the presentation
### Scripted
- `./scripts/Restore-Presentation.ps1` - restores the dependencies and tools of the presentation
- `./scripts/Start-Presentation.ps1` - runs the presentation
- `./scripts/Test-Presentation.ps1` - verifies the consistency of the Markdown files
