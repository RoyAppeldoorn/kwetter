# Kwetter front-end

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).

## Firebase setup
add a ```.env.local``` file with the following content after registering a firebase auth project. The gateway host address is based on the port it is running. The default is 5000.


```
VUE_APP_FIREBASE_API_KEY=<KEY>
VUE_APP_FIREBASE_AUTH_DOMAIN=<DOMAIN>
VUE_APP_FIREBASE_PROJECT_ID=<ID>
VUE_APP_FIREBASE_STORAGE_BUCKET=<LINK>
VUE_APP_FIREBASE_MESSAGING_SENDER_ID=<ID>
VUE_APP_FIREBASE_APP_ID=<ID>

VUE_APP_GATEWAY_HOST=<ADDRESS>
```
