const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "https://localhost:44359",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
