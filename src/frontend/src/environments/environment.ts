// For local development, please clone this file as environment.local.ts and change the values as needed.

export const environment = {
  production: false,
  url: 'http://localhost:4200',
  api: 'https://localhost:7148',
  googleMapsKey: 'GOOGLE_MAPS_KEY_HERE',
  auth: {
    authority: 'https://ingoodhands.eu.auth0.com',
    audience: 'ingoodhands',
    clientId: 'KvCQmpdmay63JC5N5ObiK4DuKzVBys7L',
    scopes: 'openid profile offline_access email api',
  }
};