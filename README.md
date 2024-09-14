### GkoTradeService

* GkoTradeService is micro-service that was built using ASP.NET Core to retrieve the aggregated bitcoin price.

## Technologies
<ul>
  <li>.NET Core 8</li>
  <li>ASP.NET Core Web API</li>
  <li>Entity Framework Core for data access</li>
  <li>In-Memory Database for development</li>
</ul>

## Getting Started
*Clone the repository
```
git clone https://github.com/Gregory95/gkoTradeService.git
cd gkoTradeService
```

## Running the Service
* Local Development

* To run the service locally, use the following command:
```
dotnet run
```
* By default, the service will be hosted at http://localhost:5000.


## Docker
* Build and run the service in a Docker container:

## Build the Docker image:
```
docker build -t gko95/gkotradeservice .
```

## Run the container:
```
docker run -dp 127.0.0.1:8080:8080 gko95/gkotradeservice
```

## API Endpoints
* The following endpoints are available in the API:

<ul>
  <li>GET api/cryptoprices?start={datetime}Z&end={datetime} - Get Crypto Prices within the specified time range</li>
  <li>GET api/cryptoprices/aggregated-bitcoin-price?timestamp={datetime} - Retrieve the aggregated bitcoin price</li>
</ul>

