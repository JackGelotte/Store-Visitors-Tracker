# Store-Visitors-Tracker
*Logs visitor count in a store, and it's sections.*
When a customer moves from one section to another, the sensors would trigger and send to the API both sections.
The API then logs this, with a timestamp. It also keeps a count on how many people are in each area.

---
## Example 
__ A person moves from the "Kitchen"-section to the "Electronics"-section in a store. 
The sensor sends "Electronics.Kitchen" to the API. 
The API creates a log, {Enter: Electronics, Exit: Kitchen, Timestamp: 09:45}.
Then it changes the visitor count of each area accordingly.
---
