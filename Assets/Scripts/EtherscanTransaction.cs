using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherscanTransaction : MonoBehaviour 
{
    public string tokenType;
    public string from;
    public string contractAddress;
    public string to;
    public string Owner;
    public string tokenID;
    public string URI;
    public string pictureURL;


    public EtherscanTransaction(Token_DataModel data)
    {
        tokenType = "etherscan";
        from = data.from;
        contractAddress = data.contractAddress;
        to = data.to;
        tokenID = data.tokenID;
    }
}
