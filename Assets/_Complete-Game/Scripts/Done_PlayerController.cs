using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
    public float boltSpeed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
    public InputField inputField;

    public void Fire(Vector3 target)
    {
        GameObject shotObj = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Rigidbody shotRB = shotObj.GetComponent<Rigidbody>();
        shotObj.transform.LookAt(target);
        shotRB.AddForce((target - shotObj.transform.position).normalized * boltSpeed);
        GetComponent<AudioSource>().Play();
    }

	void FixedUpdate ()
	{
	    if (!inputField.isFocused)
	    {
	        float moveHorizontal = Input.GetAxis("Horizontal");
	        float moveVertical = Input.GetAxis("Vertical");

	        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
	        GetComponent<Rigidbody>().velocity = movement * speed;

	        GetComponent<Rigidbody>().position = new Vector3
	        (
	            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
	            0.0f,
	            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
	        );

	        GetComponent<Rigidbody>().rotation =
	            Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	    }
	}
}
