using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour,IDataPersistence
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;

    public float walkSpeed;
    float x;
     float y;

    Vector2 direction;
    
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        
      
    }
    private void FixedUpdate()
    {
        body.MovePosition(body.position + direction * walkSpeed * Time.fixedDeltaTime);
    }

    public void LoadData(GameData data)
    {
        if (!data.fromShop)
        {
            this.transform.position = data.playerPosition;
        }
    }

    public void SaveData(ref GameData data)
    {
       // data.playerPosition = this.transform.position;
    }
}
