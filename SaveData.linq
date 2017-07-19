<Query Kind="Statements">
  <Connection>
    <ID>8e1781eb-e464-4966-87b6-a99132c91883</ID>
    <Persist>true</Persist>
    <Server>DAN-MBP</Server>
    <NoCapitalization>true</NoCapitalization>
    <Database>LINQPadTalkOMS</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var order = Orders.FirstOrDefault();

order.TotalPrice = 123;

SubmitChanges();
