# Lab1GED

Name: Edmond Huang

Student #: 100923160

Ball Parkour: Just a simple ball parkour game where you jump on platforms until you reach the end with a death counter. It isn't finished, but this is what I managed to make within the time span given.

Pseudocode:

CLASS GameManager : MonoBehaviour
    STATIC VARIABLE Instance = null
    VARIABLE deathScore = 0
    METHOD Awake()
        IF Instance IS NOT null AND Instance IS NOT this
            DESTROY this object
            RETURN
        ENDIF
        SET Instance = this
        MARK this object as DontDestroyOnLoad
    END METHOD
    METHOD AddDeathScore(amount)
        deathScore = deathScore + amount
        PRINT("Deaths: " + deathScore)
    END METHOD
END CLASS

CLASS Respawn : MonoBehaviour
    VARIABLE threshold
    METHOD FixedUpdate()
            IF GameManager.Instance EXISTS THEN
                CALL GameManager.Instance.AddDeathScore(1)
            ENDIF
            TELEPORT player to respawn point
        ENDIF
    END METHOD
END CLASS

I used singleton for my game manager because there is usually only one game manager in a game project. It is beneficial because it allows other scripts to access your one and only game manager.
