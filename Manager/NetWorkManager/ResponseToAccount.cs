public class ResponseToAccount
{
    public bool result;
    public Payload payload;
}

public class Payload { public string token; }


/*
=== Success Case ===
{
    "result": "success",
    "payload": {
        "token": "Json Web Token"
    }
}

=== Fail Case ===
{
"result": "failed",
"payload": {}
}


*/
