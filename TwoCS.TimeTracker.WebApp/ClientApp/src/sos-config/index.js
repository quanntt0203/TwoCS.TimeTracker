const development = {
  apiGateway: {
      API_URL: "http://localhost:8000",
  }
};

const staging = {
  apiGateway: {
      API_URL: "",
  }
};

const config =
  process.env.NODE_ENV === "development" ? development : staging;

export default config;
