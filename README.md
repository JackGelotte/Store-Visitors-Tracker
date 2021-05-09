# Store-Visitors-Tracker
*Logs visitor count in a store, and it's sections.*

---

When a customer moves from one section to another, the sensors would trigger and send to the API both sections.
The API then logs this, with a timestamp. It also keeps a count on how many people are in each area.
You can also call the API to get all sections and the current count on each section.
And do a reset on a section.

---

#### Example ####
A person moves from the "Kitchen"-section to the "Electronics"-section in a store. 
The sensor sends "Electronics.Kitchen" to the API. 
The API creates a log, *Enter: Electronics, Exit: Kitchen, Timestamp: 09:45*.
Then it changes the visitor count of each area accordingly.

✨Issues✨
- Currently doesn't deal with an entrance. i.e no visitors enter or leave the actual store. Works with mock-data for now.
- A visitor can be in any section, and enter ANY section from it. Which is highly unlikely in a real scenario. However real sensors would deal with this.
- Missing a lot of errorhandling.
