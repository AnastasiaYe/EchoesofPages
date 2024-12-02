using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public float cameraSpeed;
    private int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += new Vector3(cameraSpeed*dir,0,0);


    }

    private void Update()
    {
        if (this.transform.position.x <= -28)
        {
            dir = 1;
        }
        else if (this.transform.position.x >= 5.3)
        {
            dir = -1;
        }
    }

    public void GameStart() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
        
    
}
