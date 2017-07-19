<Query Kind="Statements">
  <Connection>
    <ID>8e1781eb-e464-4966-87b6-a99132c91883</ID>
    <Persist>true</Persist>
    <Server>DAN-MBP</Server>
    <NoCapitalization>true</NoCapitalization>
    <Database>LINQPadTalkOMS</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <NuGetReference>EnyimMemcached</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Enyim.Caching</Namespace>
  <Namespace>Enyim.Caching.Configuration</Namespace>
  <Namespace>Enyim.Caching.Memcached</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var config = new MemcachedClientConfiguration();

config.Servers.Add(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11211));
config.Protocol = MemcachedProtocol.Binary;

var mc = new MemcachedClient(config);

mc.Store(StoreMode.Set, "MyKey", 12345, TimeSpan.FromSeconds(5));
var value = mc.Get("MyKey");

value.Dump();
