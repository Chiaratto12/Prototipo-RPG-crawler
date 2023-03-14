using System.Collections.Generic;
public class Enemy {
    public string name;
    public int life;
    public int damage;
    public float evasionChance;
    public float critChance;
    public int xp;
}

public class Boss {
    public string name;
    public int life;
    public int damage;
    public float evasionChance;
    public float critChance;
    public int xp;
}

public class EnemyList {
    public List<Enemy> Enemy;
}

public class BossList {
    public List<Boss> Boss;
}