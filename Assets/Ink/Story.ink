int friendship = 0  // (int) _inkStory.variablesState["NPCState.Friendship"]
string class = Warrior //(string) _inkStory.variablesState["NPCState.Class"]

=== tutorial ===
You see someone hard at work...
"Hello, you must be the new owner of the inn. Would you like an explanation about thing?"

 * Yes, I'm new here can you explain.
 * No, I already know what to do.

- They did what they done
    -> end_day

=== end_day ===
The sun is starting to set. We better go inside
 -> END
