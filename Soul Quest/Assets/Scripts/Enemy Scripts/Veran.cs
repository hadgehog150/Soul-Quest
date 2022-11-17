using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Veran : MonoBehaviour
{
    public float speed;
	public int hp;

	private bool attacing;
	public float attackSpeed, attackSpeed2;

	public float maxRandomPositionX, minRandomPositionX;
	public float maxRandomPositionY, minRandomPositionY;

	private Vector3 initialPosition;

	[SerializeField]
	int numberOfProjectiles;

	[SerializeField]
	GameObject projectile;

	Vector2 startPoint;

	float radius, moveSpeed;

	// Start is called before the first frame update
	void Start()
    {
        initialPosition = transform.position;
		radius = 5f;
		moveSpeed = 5f;
	}

    // Update is called once per frame
    void Update()
    {
        if (hp<=0)
        {
			Destroy(gameObject);
			SceneManager.LoadScene(0);
		}

        transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, initialPosition) == 0)
        {
            initialPosition = new Vector2(Random.RandomRange(maxRandomPositionX, minRandomPositionX), Random.RandomRange(maxRandomPositionY, minRandomPositionY));
        }

		if (!attacing) {
			startPoint = transform.position; 
			StartCoroutine(Attack(attackSpeed));
		}
		
	}

	IEnumerator Attack(float seconds)
	{
		attacing = true;
		if (projectile != null)
		{
			SpawnProjectiles(numberOfProjectiles);
			
			yield return new WaitForSeconds(seconds);
		}
		attacing = false;
	}

	IEnumerator Attacking(float seconds)
	{
		attacing = true;
		yield return new WaitForSeconds(seconds);
		attacing = false;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Attack")
		{
			--hp;
		}

		if (collision.tag == "Player")
		{
			if (!attacing)
			{
				StartCoroutine(Attacking(attackSpeed2));
				collision.SendMessage("Attacked");
			}

		}
	}

	void SpawnProjectiles(int numberOfProjectiles)
	{
		float angleStep = 360f / numberOfProjectiles;
		float angle = 0f;

		for (int i = 0; i <= numberOfProjectiles - 1; i++)
		{

			float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
			float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

			Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
			Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

			var proj = Instantiate(projectile, startPoint, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity =
				new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

			angle += angleStep;
		}
	}

	public void Attacked()
	{
		--hp;
	}
}
