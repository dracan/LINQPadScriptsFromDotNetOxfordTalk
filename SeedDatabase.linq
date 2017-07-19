<Query Kind="Program">
  <Connection>
    <ID>8e1781eb-e464-4966-87b6-a99132c91883</ID>
    <Persist>true</Persist>
    <Server>DAN-MBP</Server>
    <NoCapitalization>true</NoCapitalization>
    <Database>LINQPadTalkOMS</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

User[] _users;
Product[] _products;

void Main()
{
    CreateUsers();
    CreateProducts();
    CreateOrders();

    SubmitChanges();

    "Done".Dump();
}

void CreateUsers()
{
    "Seeding users ...".Dump();

    _users = new[]
    {
        new User
        {
            ID = Guid.NewGuid(),
            FirstName = "Joe",
            LastName = "Bloggs",
        },
        new User
        {
            ID = Guid.NewGuid(),
            FirstName = "Simon",
            LastName = "Smith",
        },
    };

    Users.InsertAllOnSubmit(_users);
}

void CreateProducts()
{
    "Seeding products ...".Dump();

    _products = new[]
    {
        new Product
        {
            ID = Guid.NewGuid(),
            Name = "John Smith's Extra Smooth Bitter 4 x 440ml",
            Description = "John Smith's Extra Smooth is a bestselling British ale with distinct cereal character, combined with malt and caramel. Equal amounts of fruit and bitterness create a balanced tasting ale with a smooth, creamy texture. Brewed in Tadcaster, North Yorkshire since 1758 John Smith's is the number 1 ale in the UK",
            Price = 3.50m,
            ImageUri = "https://www.ocado.com/productImages/164/16428011_0_640x640.jpg?identifier=f59b379474d1bad7314396cfbe0f64c2",
        },    
        new Product
        {
            ID = Guid.NewGuid(),
            Name = "San Miguel 4 x 330ml",
            Description = "San Miguel is Spain's biggest export beer brand. It was first brewed in the Lleida brewery in Spain in 1957. San Miguel is brewed using a special mashing process which gives the beer a fuller and slightly sweeter flavour. The unique recipe and San Miguel yeast then come together to deliver a refreshing, full bodied beer with a great taste. San Miguel is a pilsner style lager, golden in colour, sparkling with a generous white creamy head. San Miguel's philosophy is simple 'A passion that if you live your life with curiosity, optimism and a belief that the best is yet to come; then great things will happen'. And the best is yet to come",
            Price = 4.10m,
            ImageUri = "https://www.ocado.com/productImages/315/31582011_0_640x640.jpg?identifier=1c8fec2f09acc498d0e71eb9e5146505",
        },
        new Product
        {
            ID = Guid.NewGuid(),
            Name = "Peroni Nastro Azzurro Piccola 6 x 250ml",
            Description = "Begin your evening in Italian style. The Peroni Nastro Azzurro Piccola bottle offers a stylish, unique and unquestionably Italian way to start your evening. Created for those special nights where style, taste and enjoyment are paramount, enjoy Piccola on its own or as the perfect accompaniment to aperitivo dishes. www.thehouseofperoni.com",
            Price = 7.50m,
            ImageUri = "https://www.ocado.com/productImages/235/235445011_0_640x640.jpg?identifier=21d59296ad43807d44a83cc7a3e8ca10",
        },
    };

    Products.InsertAllOnSubmit(_products);
}

void CreateOrders()
{
    "Seeding orders ...".Dump();

    CreateOrder(new Dictionary<Product, int> { { _products[0], 1 }, { _products[2], 5 } }, _users[0].ID);
    CreateOrder(new Dictionary<Product, int> { { _products[1], 5 } }, _users[0].ID);
    CreateOrder(new Dictionary<Product, int> { { _products[2], 2 } }, _users[0].ID);
}

void CreateOrder(Dictionary<Product, int> items, Guid userId)
{
    var order = new Order
    {
        ID = Guid.NewGuid(),
        UserID = userId,
    };
    
    var orderItems = items.Select(x => new OrderItem
    {
        ID = Guid.NewGuid(),
        OrderID = order.ID,
        ProductID = x.Key.ID,
        Quantity = x.Value,
        SalePrice = x.Key.Price,
    });

    OrderItems.InsertAllOnSubmit(orderItems);

    order.TotalPrice = orderItems.Sum(oi => oi.SalePrice * oi.Quantity);

    Orders.InsertOnSubmit(order);
}