using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token
{
    public string tokentype;
    public string owner;
    public string contractAddress;
    public string tokenId;
    public string description;
    public string image;

    public Token(Bounce_DataModel.Nfts1155 data)
    {
        tokentype = data.token_type;
        owner = data.owner_addr;
        contractAddress = data.contract_addr;
        tokenId = data.token_id;
        description = data.description;
        image = data.image;
    }

    public Token(Bounce_DataModel.Nfts721 data)
    {
        tokentype = data.token_type;
        owner=data.owner_addr;
        contractAddress=data.contract_addr;
        tokenId=data.token_id;
        description=data.description;
        image=data.image;
    }
}
