using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Round {
	public int match_id;
	public int round_id;
	public int length;
	public int width;
	public string team_0;
	public string team_1;
	public int defenders;
	public int midfield;
	public int attacker;
	public string commentary;
	public string background;
	public CellEnum[ , ] field;

	public Round(){
		this.length = 10;
		this.width = 10;
	}

	public Round(int lendth, int width){
		this.length = lendth;
		this.width = width;
	}

	public void setFieldDimensions(){
		this.field = new CellEnum[this.width, this.length];
	}

	public string asString(){
		string s =  "Current field : \n";
		s += "Match : "+this.match_id+"\n";
		s += "round_id : "+this.round_id+"\n";
		s += "m_length : "+this.length+"\n";
		s += "m_width : "+this.width+"\n";
		s += "team_0 : "+this.team_0+"\n";
		s += "team_1 : "+this.team_1+"\n";
		s += "defenders : "+this.defenders+"\n";
		s += "midfield : "+this.midfield+"\n";
		s += "attacker : "+this.attacker+"\n";
		s += "commentary : "+this.commentary+"\n";
		s += "field : \n";

		for (int i = 0; i < this.length; i++) {
			for (int j = 0; j < this.width; j++) {
				s += this.field [j, i];
			}
			s += "\n";
		}

		return s;
	}
}
