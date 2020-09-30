# Introduction
A very simple UI for the [CQRS AES Key API](https://github.com/briandenicola/cqrs)


# Setup
## Prerequisite
* Deploy [CQRS AES Key API](https://github.com/briandenicola/cqrs)

## Mock Environment & Infrastructure 
* cqrs-ui/infrastructure/create_mock_api_environment.sh -g {Resource Group} -l {location} -s {Subscription Name}
    * Will create an Azure Function and Azure APIM Consumption Plan
    * Will deploy a mock of the [CQRS Api](https://github.com/briandenicola/cqrs) to the Azure Function
    * After creation, manually create an API in APIM that points to the Azure Function. Add a CORS policy to the API inbound processing

## Code Deployment 
* Fork https://github.com/briandenicola/cqrs-ui
    * https://docs.github.com/en/enterprise-server@2.20/github/getting-started-with-github/fork-a-repo
* az staticwebapp create -n bjdweb001 \
    -g Dev_Static_WebApp \
    -s https://github.com/{userid}/cqrs-ui \
    -l centralus \
    -b master \
    --app-location /src/ui \
    --app-artifact-location wwwroot \
    -t {Github PAT}

# Roadmap
- [x] Create Mock API environment
- [x] Simple Blazor UI deployed to Azure Static Web Apps
- [x] Automate Azure Static Web Apps infrastructure
- [x] Automate deploy to Azure Static Web Apps
- [ ] ~~Update code for SignalR with Blazor~~ 
- [ ] ~~Migrate runtime to Azure SignalR service using Azure Cosmos Change Feed/Functions~~
- [ ] ~~Automate Azure SignalR infrastructure~~ 
- [ ] ~~Automate deploy Azure SignalR~~ 
- [ ] Integrate Authentiction with Azure AD B2C

# Known Issues
- None
