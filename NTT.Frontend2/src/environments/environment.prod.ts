
export const environment = {
  production: true,
  apiUrl: 'https://nttapiwebdeploy.azurewebsites.net/api',
  azureAnimalImageUrl : 'https://minstagram.blob.core.windows.net/images/',

  identityProvider: {
    stsAuthority: "https://nttidentity20211121160220.azurewebsites.net",
    clientId: "minstagram.web",
    clientRoot: "https://minstagramweb.azurewebsites.net",
    clientScope: "openid minstagram.api"
  }
};
