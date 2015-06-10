using UnityEngine;
using System.Collections;

public class PokemonMgr : MonoBehaviour {


	public  Pokemon[] pokemon;
	public  Pokemon enemy;

	// Use this for initialization

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public  void generateEnemy()
	{
		int pokeNo = Random.Range(0,51);
		if(pokeNo != 50)
		{
			pokeNo = pokeNo % 21;
		}
		else
		{
			pokeNo = Random.Range(21,27);
		}

		enemy = Instantiate(pokemon[pokeNo]);
		if(enemy.name == GameObject.Find("Player").GetComponent<Player>().curPokemon.name)
		{
			this.generateEnemy();
		}
	}

	public void battleEnd()
	{
		Destroy(this.gameObject);
	}
}
