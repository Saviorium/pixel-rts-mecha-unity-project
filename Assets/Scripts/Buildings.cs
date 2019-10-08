using System.Collections.Generic;
using UnityEngine;

    public class Buildings{
		
		public int health;
		public string name;
		public GameObject avatar;
 
		public Buildings(int _health, string _name, GameObject _avatar){
			health = _health;
			name = _name;
			avatar = _avatar;
			
		}
	}