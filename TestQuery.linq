<Query Kind="Expression">
  <Connection>
    <ID>8e1781eb-e464-4966-87b6-a99132c91883</ID>
    <Persist>true</Persist>
    <Server>DAN-MBP</Server>
    <NoCapitalization>true</NoCapitalization>
    <Database>LINQPadTalkOMS</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

Orders
.Select(x => new
{
    x.ID,
    x.TotalPrice,
    UserName = $"{x.User.FirstName} {x.User.LastName}",
    Items = x.OrderItems.Select(oi => new
    {
        oi.Product.Name,
        Image = Util.Image(oi.Product.ImageUri),
    })
})
.FirstOrDefault(x => x.Items.Any(i => i.Name.Contains("San Miguel")))
