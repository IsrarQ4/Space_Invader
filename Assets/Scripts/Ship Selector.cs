using UnityEngine;
using UnityEngine.UI;

public class ShipSelector : MonoBehaviour
{
    public GameObject selectionPanel;
    public Button ship1Button;
    public Button ship2Button;
    public Button ship3Button;
    public Button ship4Button;
    
    public Player player;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectionPanel.SetActive(true);
 
        ship1Button.onClick.AddListener(() => SelectShip(1));
        ship2Button.onClick.AddListener(() => SelectShip(2));
        ship3Button.onClick.AddListener(() => SelectShip(3));
        ship4Button.onClick.AddListener(() => SelectShip(3));

    }
    void SelectShip(int shipNumber)
    {

        player.SelectSpaceship(shipNumber);

        selectionPanel.SetActive(false);

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
