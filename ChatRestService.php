<?php
    require "RestService.php";

class ChatRestService extends RestService
{
    public function __construct()
    {
        parent::__construct("");
    }

    public function performPost($url, $parameters, $requestBody, $accept)
    {
        switch (count($parameters))
        {
            case 1:
                if($parameters[0] == "ChatProcess")
                {
                    $jsonData = json_decode($requestBody);
                    $function = $jsonData->{'function'};
                    switch ($function)
                    {
                        case 'getState':
                            $chatProcess = new ChatProcess();
                            $chatProcess.GetState();
                            break;

                        case 'update':
                            $state = $jsonData->{'state'};
                            $chatProcess = new ChatProcess();
                            $chatProcess.Update($state);
                            break;

                        case 'send':
                            $message = $jsonData->{'message'};
                            $nickname = $jsonData->{'nickname'};
                            $chatProcess = new ChatProcess();
                            $chatProcess.Send($message,$nickname);
                            break;
                    }
                }
        }
    }
}
?>


