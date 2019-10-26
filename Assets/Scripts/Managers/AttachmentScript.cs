using Enums;
using UnityEngine;

public class AttachmentScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ 
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
               print(hit.transform.name);
            }
        }
    }

    public void OnUserInput(InputDirection direction)
    {
        print(direction);
    }
}
