### This Docs is still under development


### Class `SpeechToTextHandler`

Public Attributes (this will be soon changed to getter and setter): 

* `outputText`: takes in TextMeshProUGUI that used to print Speech debug log.
* `startRecoButton`: takes in Button that trigger start Speech listener.


### Class `CLUHandler`

```
private static Uri endpoint = new Uri("https://handson-language.cognitiveservices.azure.com/");
private static AzureKeyCredential credential = new AzureKeyCredential(#your key here);
```

Public Attributes (this will be soon changed to getter and setter): 

* `Instance`: static singleton CLUHandler instance.
* `m_logTmp`: takes in TextMeshProUGUI that used to print CLU debug log.
* `WorkSpaceParent`: workspace safety parent Transform that rotated according to initialization, used to spawn stuff into.
* `SpawnPos`: workspace spawn position to fit the table in the real world.

Public Methods:

* void `AnalyzeConversation(string prompt)`: input prompt then print output to `m_logTmp`
