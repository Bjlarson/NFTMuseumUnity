using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_DataModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Nfts1155
    {
        public string contract_addr { get; set; }
        public string contract_name { get; set; }
        public string token_type { get; set; }
        public string token_id { get; set; }
        public string owner_addr { get; set; }
        public string balance { get; set; }
        public string token_uri { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public object metadata { get; set; }
    }

    public class Attribute
    {
        public string trait_type { get; set; }
        public string value { get; set; }
    }

    public class Metadata
    {
        public List<Attribute> attributes { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public long? date { get; set; }
        public string dna { get; set; }
        public int? edition { get; set; }
    }

    public class Nfts721
    {
        public string contract_addr { get; set; }
        public string contract_name { get; set; }
        public string token_type { get; set; }
        public string token_id { get; set; }
        public string owner_addr { get; set; }
        public string balance { get; set; }
        public string token_uri { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Data
    {
        public List<Nfts1155> nfts1155 { get; set; }
        public List<Nfts721> nfts721 { get; set; }
        public int total1155 { get; set; }
        public int total721 { get; set; }
    }

    public class Root
    {
        public int code { get; set; }
        public Data data { get; set; }
        public string msg { get; set; }
    }
}
