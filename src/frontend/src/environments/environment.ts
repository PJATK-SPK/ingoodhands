// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  url: 'http://localhost:4200',
  api: 'https://localhost:7148',
  googleMapsKey: 'AIzaSyA_VzFjbACVq7tV0arn0uVcgoRuukFs1fI',
  auth: {
    authority: 'https://ingoodhands.eu.auth0.com',
    audience: 'ingoodhands',
    clientId: 'KvCQmpdmay63JC5N5ObiK4DuKzVBys7L',
    scopes: 'openid profile offline_access email api',
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 * -> import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
 */
