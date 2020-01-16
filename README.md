<img src="docs/trolleymotion_eu.png" alt="trolleymotion.eu logo" width="150px" />

# PublicTransportRealtime

![Version Badge](https://img.shields.io/github/v/tag/DenisBalan/PublicTransportRealtime)
![License Badge](https://img.shields.io/github/license/DenisBalan/PublicTransportRealtime)
![Code size Badge](https://img.shields.io/github/languages/code-size/DenisBalan/PublicTransportRealtime)

Demo project written in Blazor (.NET web framework) that shows realtime map of Chisinau public transport (feel free to fork it on github)

Realtime monitoring of public transport (for the moment from Chisinau only)

Demo (hosted by Azure):  https://publictransportmoldova.azurewebsites.net/
![Public Transport Realtime Chisinau][demo]


Getting Started
-----------

Run via ```docker run -it -p 3333:80 denisbalan/public-transport-realtime:v0.1a```

Browse to http://localhost:3333 to view the page within container. You should see a page similar to demo gif above.

# Workflow

This project uses gitflow, here some documentation about [how gitflow works](https://datasift.github.io/gitflow/IntroducingGitFlow.html)

## Pull Requests

Code contributions are greatly appreciated, please make sure you follow the guidelines below.

## Pull requests should be
1. Made against the `development` branch.
1. Made from a git feature/fix branch.
1. Associated to a documented issue.

Similar projects
-----------
* [https://roataway.md](https://roataway.md)
* [https://troleibuze.md](https://troleibuze.md) (same as roataway?)
* [https://eway.md](https://eway.md)

Credits
-----------
* [https://www.dekart.com/](https://www.dekart.com/) - for providing opendata
* [https://www.map.md/](https://www.map.md/) - for detailed Chisinau map layer
* [https://github.com/roataway](https://github.com/roataway) - for providing idea of this project
* [https://trolleymotion.eu/](https://trolleymotion.eu/) - for populating idea of green public transport

*Made in Chisinau with ❤*

[demo]: https://user-images.githubusercontent.com/33955091/72536670-58ce2e80-3883-11ea-871c-240c9b6b0d1f.gif "Chisinau realtime public transport"