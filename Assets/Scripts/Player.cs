using UnityEngine;

public class Player : MonoBehaviour
{
    private Spaceship _ChosenSpaceship;
   void Start()
    {
    Spaceship Spaceship1 = new Spaceship(10, 120, 3);

    Spaceship Spaceship2 = new Spaceship(20, 70, 4);

    Spaceship Spaceship3 = new Spaceship(5, 150, 5);

    Spaceship Spaceship4 = new Spaceship(15, 100, 2);

    _ChosenSpaceship = Spaceship1;
    }

   void Update()
   {
    int speed = _ChosenSpaceship.GetMovementSpeed();

    if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Move right
    if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
   

   }

public class Spaceship
    {
    private int _movementspeed ;
   private float _player_health;
   private int _bullet_speed;

   public Spaceship(int movespeed, int playerhealth, int bulletspeed){
   
   this._movementspeed = movespeed;
   this._player_health = playerhealth;
   this._bullet_speed = bulletspeed;
   }

    public int GetMovementSpeed()
    {
        return _movementspeed;
    }

    public float GetPlayerHealth()
    {
        return _player_health;
    }

    public int GetBulletSpeed()
    {
        return _bullet_speed;
    }
    }
  
    }
  


