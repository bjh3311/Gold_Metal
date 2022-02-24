using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ranking : MonoBehaviour
{
    // Start is called before the first frame update
    private string RankingUrl;
    private void Awake()
    {
        RankingUrl="bjh3311.cafe24.com/Ranking.php";
    }

    // Update is called once per frame
    public void Back()
    {
        SceneManager.LoadScene("Main");
    }
}
