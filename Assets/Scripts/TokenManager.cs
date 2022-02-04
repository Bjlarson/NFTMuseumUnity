using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class TokenManager : MonoBehaviour
{
    public string WEB_URL = "";
    public List<Token> tokens = new List<Token>();

    public List<GameObject> spawnpoints = new List<GameObject>();
    public GameObject PortratePictureFrame;
    public GameObject PictureFrame;

    //erc1155 token function to get uri info
    //function uri(uint256 _id) external view returns(string memory);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTokenInfo());
    }

    IEnumerator GetTokenInfo()
    {
        WEB_URL = $"https://nftview.bounce.finance/v2/main/nft?user_address=0x79f261f483b7cef4f995c1f8a0f46f88450423e3";// +GalleryInfo.walletAddress+ "&startblock=0&endblock=999999999&sort=asc&apikey=8CRTZI63W982XC7MR5R7BHQN2C7GTBYPU8";

        UnityWebRequest rq = UnityWebRequest.Get(WEB_URL);
        yield return rq.SendWebRequest();
        var jsonResult = rq.downloadHandler.text;
        Debug.Log(jsonResult);
        Bounce_DataModel.Root data = JsonConvert.DeserializeObject<Bounce_DataModel.Root>(jsonResult);//JsonUtility.FromJson<Bounce_DataModel.Root>(jsonResult);
        Debug.Log(data.data.ToString());
        if (data == null)
        {
            Debug.Log("failed to get root info"+jsonResult);
        }
        Debug.Log("gathered data");

        for (int i = 0; i < data.data.total721; i++)
        {
            Debug.Log("started erc 721" + data.data.nfts721[i]);
            var token = new Token(data.data.nfts721[i]);

            Debug.Log(token.image);
            Regex.Match(token.image, "ipfs://").Value.Replace("ipfs://", "https://ipfs.io/ipfs/");
            Debug.Log(token.image);

            Texture2D texture = null;
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(token.image);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
            {
                texture = DownloadHandlerTexture.GetContent(request);
            }

            if (texture != null)
            {
                if (texture.height > texture.width)
                {
                    Debug.Log("picture is portrat " + texture.height.ToString());
                }
            }

            var Picture = Instantiate(PictureFrame, spawnpoints[i].transform);

            Picture.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", texture);
        }

        for (int i = 0; i < data.data.total1155; i++)
        {
            Token token = new Token(data.data.nfts1155[i]);

            if (token.image == null || token.image == "")
            {
                Debug.Log("get ERC1155 image");
            }


            tokens.Add(token);
        }
    }
}
