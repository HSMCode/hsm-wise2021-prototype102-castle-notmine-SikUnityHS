using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public int coins = 0;
    public int speed = 3;
    public int jumps = 1;
    public int JumpForce = 7;

    public Rigidbody _rigidbody;


    /* To Do (Dominik)
     * 
     * Muenzen haben aktuell noch eine "harte Kollision", also prallt der Spieler von ihnen ab
     * Muenzen werden manchmal mehrfach gewertet
     * Skriptstruktur ueberarbeiten: Movement, UI, Coins in verschiedene Skripte auslagern
     */ 



    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode.Impulse);
            jumps--;
        }

            Text coinText = GameObject.Find("Canvas/Text").GetComponent<Text>();
            coinText.text = "Coins: " + coins + "/3";   
    }


    void OnCollisionEnter(Collision collision)
    {
        
      if (collision.gameObject.name == "Coin")
        {
           Destroy(collision.gameObject);
           coins++;
        }

        if (collision.gameObject.name == "Floor" && jumps == 0)
        {
            jumps = 1;
        }

        if (collision.gameObject.name == "LevelBorder")
        {
            transform.Rotate(Vector3.down * 180);
        }

        if (collision.gameObject.name == "Finish" && coins >= 3)
        {
            Text winText = GameObject.Find("Canvas/WinMessage").GetComponent<Text>();
            winText.text = "You win!";
        }

    }
}
