
using UnityEngine;

public class Scorer : MonoBehaviour
{
    //Score tmprotext
    int score;
    public TMPro.TextMeshProUGUI scoreText;
    //self box collider
    public playercontrol player;


    // Start is called before the first frame update
    public void Update()
    {
        score = player.score;
        scoreText.text = "Score: " + score;


    }


}
