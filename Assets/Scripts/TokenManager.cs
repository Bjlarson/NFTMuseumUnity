using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class TokenManager : MonoBehaviour
{
    public string WEB_URL = "";
    public List<Token> tokens = new List<Token>();

    public GameObject PortratePictureFrame;
    public GameObject PictureFrame;

    //erc1155 token function to get uri info
    //function uri(uint256 _id) external view returns(string memory);

    // Start is called before the first frame update
    void Start()
    {
        WEB_URL = $"https://nftview.bounce.finance/v2/main/nft?user_address=0x5B2bFA4735B3209df2c12118Bb971C88292c3d75";// +GalleryInfo.walletAddress+ "&startblock=0&endblock=999999999&sort=asc&apikey=8CRTZI63W982XC7MR5R7BHQN2C7GTBYPU8";

        UnityWebRequest rq = UnityWebRequest.Get(WEB_URL);

        string jsonResult = System.Text.Encoding.UTF8.GetString(rq.downloadHandler.data);
        Bounce_DataModel.Root data = JsonUtility.FromJson<Bounce_DataModel.Root>(jsonResult);
        if (data == null)
        {
            Debug.Log(jsonResult);
            return;
        }

        List<Token> list = new List<Token>();

        for (int i = 0; i < data.data.total721; i++)
        {
            var token = new Token(data.data.nfts721[i]);

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(token.image);
            request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
            {
                token.pictureMaterial = DownloadHandlerTexture.GetContent(request);
            }

            if(token.pictureMaterial.height > token.pictureMaterial.width)
            {
                Debug.Log("picture is portrat");
            }

            token.prefab = PictureFrame;

            list.Add(token);
        }

        for (int i = 0; i < data.data.total1155; i++)
        {
            Token token = new Token(data.data.nfts1155[i]);

            if(token.image == null || token.image == "")
            {
                Debug.Log("get ERC1155 image");
            }


            list.Add(token);
        }

        foreach(var token in list)
        {
            Instantiate(token);
        }
    }
}
