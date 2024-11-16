using System;
using System.Collections.Concurrent;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using Azure;

//using Microsoft.Azure.Cosmos.Table;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;


string storageUri = "https://csop139.table.core.windows.net";
string storageAccountKey = "PnxT/M65SAZ4aqwN2XPK4BvmDnG91fMsA58wuRbK535vGvn1utNUe3/ybTcG8kCEEmWaBRo9Gxxx6VlXgAoQOQ==";
string accountName = "csop139";
string tableName = "MyTestTable3";
string partitionKey = "AAA";
string rowKey = "12";


//POST_New_Table();
GET_Table();
//POST_New_Entity();
//GET_Entity();

void GET_Table()
{
    var serviceClient = new TableServiceClient(
        new Uri(storageUri),
        new TableSharedKeyCredential(accountName, storageAccountKey));

    //Use the<see cref="TableServiceClient"> to query the service. Passing in OData filter strings is optional.

    Pageable<TableItem> queryTableResults = serviceClient.Query(filter: $"TableName eq '{tableName}'");

    Console.WriteLine("The following are the names of the tables in the query results:");

    // Iterate the <see cref="Pageable"> in order to access queried tables.

    foreach (TableItem table in queryTableResults)
    {
        Console.WriteLine(table.Name);
    }
}

void GET_Entity()
{
    var tableClient = new TableClient(
        new Uri(storageUri),
        tableName,
        new TableSharedKeyCredential(accountName, storageAccountKey));


    Pageable<TableEntity> queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");

    // Iterate the <see cref="Pageable"> to access all queried entities.
    foreach (TableEntity qEntity in queryResultsFilter)
    {
        Console.WriteLine($"{qEntity.GetString("Product")}: {qEntity.GetDouble("Price")}");
    }

    Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");
}

//Creates new table to Storage Account Table
void POST_New_Table()
{
    var serviceClient = new TableServiceClient(
        new Uri(storageUri),
        new TableSharedKeyCredential(accountName, storageAccountKey));

    TableItem table = serviceClient.CreateTableIfNotExists(tableName);
    Console.WriteLine($"The created table's name is {table.Name}.");
}

//Inserts an identity to Storage Account Table
void POST_New_Entity()
{
    //Construct a new < see cref = "TableClient" /> using a<see cref= "TableSharedKeyCredential" />.
    var tableClient = new TableClient(
        new Uri(storageUri),
        tableName,
        new TableSharedKeyCredential(accountName, storageAccountKey));

    var tableEntity = new TableEntity(partitionKey, rowKey)
    {
        { "Product", "Patata" },
        { "Price", 55.00 },
        { "Quantity", 211 }
    };

    Console.WriteLine($"{tableEntity.RowKey}: {tableEntity["Product"]} costs ${tableEntity.GetDouble("Price")}.");

    tableClient.AddEntity(tableEntity);
}
































//var serviceClient = new TableServiceClient(
//    new Uri(storageUri),
//    new TableSharedKeyCredential(accountName, storageAccountKey));

//CREATE TABLE
//TableItem table = serviceClient.CreateTableIfNotExists(tableName);
//Console.WriteLine($"The created table's name is {table.Name}.");


//GET table
//Use the<see cref="TableServiceClient"> to query the service. Passing in OData filter strings is optional.

//Pageable<TableItem> queryTableResults = serviceClient.Query(filter: $"TableName eq '{tableName}'");

//Console.WriteLine("The following are the names of the tables in the query results:");

//// Iterate the <see cref="Pageable"> in order to access queried tables.

//foreach (TableItem table in queryTableResults)
//{
//    Console.WriteLine(table.Name + "AAAAAAAAAAAA");
//}

//GET Identities
//var tableClient = new TableClient(
//    new Uri(storageUri),
//    tableName,
//    new TableSharedKeyCredential(accountName, storageAccountKey));


//Pageable<TableEntity> queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");

//// Iterate the <see cref="Pageable"> to access all queried entities.
//foreach (TableEntity qEntity in queryResultsFilter)
//{
//    Console.WriteLine($"{qEntity.GetString("Product")}: {qEntity.GetDouble("Price")}");
//}

//Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");

//POST

// Construct a new <see cref="TableClient" /> using a <see cref="TableSharedKeyCredential" />.
//var tableClient = new TableClient(
//    new Uri(storageUri),
//    tableName,
//    new TableSharedKeyCredential(accountName, storageAccountKey));



//var tableEntity = new TableEntity(partitionKey, rowKey)
//{
//    { "Product", "Marker Set" },
//    { "Price", 5.00 },
//    { "Quantity", 21 }
//};

//Console.WriteLine($"{tableEntity.RowKey}: {tableEntity["Product"]} costs ${tableEntity.GetDouble("Price")}.");

//tableClient.AddEntity(tableEntity);

//GETS an identities from Storage Account Table
