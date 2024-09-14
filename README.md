<h1>GkoTradeService</h1>

<p>GkoTradeService is micro-service that was built using ASP.NET Core to retrieve the aggregated bitcoin price.</p>

<h3>Technologies</h3>
<ul>
  <li>.NET Core 8</li>
  <li>ASP.NET Core Web API</li>
  <li>Entity Framework Core for data access</li>
  <li>In-Memory Database for development</li>
</ul>

<h3>Getting Started</h3>
<h4>Clone the repository</h4>
git clone https://github.com/Gregory95/gkoTradeService.git
<br>
cd gkoTradeService
<br>

Running the Service
<br>
Local Development
<br>
To run the service locally, use the following command:
<br>

dotnet run
<br>
By default, the service will be hosted at http://localhost:5000.
<br>

Docker (Optional)
<br>
Build and run the service in a Docker container:
<br>

<h3>Build the Docker image:</h3>
docker build -t gko95/gkotradeservice .

<h3>Run the container:</h3>
docker run -dp 127.0.0.1:8080:8080 gko95/gkotradeservice

<h3>API Endpoints</h3>
The following endpoints are available in the API:

<ul>
  <li>**GET** api/cryptoprices?start={datetime}Z&end={datetime} - Get Crypto Prices within the specified time range</li>
  <li>**GET** api/cryptoprices/aggregated-bitcoin-price?timestamp={datetime} - Retrieve the aggregated bitcoin price</li>
</ul>

