VAR class = ""
VAR friendship = 0
VAR name = ""
VAR day = 1
VAR nextPathString = ""

// Are our 4 gates open?
VAR appleGate = false
VAR cherryGate = false
VAR vanillaGate = false
VAR chocolateGate = false


-> first_day

=== first_day
// FIRST DAY *****************************************
// tutorial dialogue
After a long trip from the city you finally arrive at your new inn. You see people outside. Maybe you should try talking to them and introduce yourself. This feels like the start of something wonderful.
+ [Continue]
~ nextPathString = "first_day.tutorial"
-> DONE


= tutorial
"Hello, you must be the new owner. Would you like an explanation about running thing?"
* "Yes, I'm new here[".] can you explain."
* "No, I already know what to do"-> end_tutorial
- "Of course, as the new owner you are responsible for gathering ingredients for the inn's apothecary and bakery. You will find everything you need around outside. You'll have the whole day to gather since the inn doesn't start service till the evening."
- (questions)
* "What sort of ingredients[".] do I need to gather?" 
"The ingredients you will need are: Apples, Cherries, Eggs, Cocoa beans, Milk, Salt, Sugar Canes, Tea Leaves, Vanilla Beans, and Wheat." -> questions
* "What will I be crafting[".] with the ingredients I gather?"
"with these ingredients you will be crafting potions and pastries to sell to the patrons of the inn." -> questions
* "Okay I think I understand[".] -> end_tutorial

= end_tutorial
"Great, since you know what you're doing I'll leave you to it."
+ [Continue]
~ nextPathString = "first_day.post_tutorial" // will loop
-> DONE


= post_tutorial
{ day >= 2 : -> second_day } // break out of loop if it is 2nd day
{
        - class == "Rogue":
            -> Rogue
        - class == "Magician":
            -> Magician
        - class == "Ranger":
            -> Ranger
        - class == "Fighter":
            -> Fighter
    }
    
- (Rogue)
{ &"If your done looking around you should go inside and get ready for the night!" | "Are you lost?" | "hmm..." }
+ [Continue]
-> DONE

- (Magician)
{ &"How are you doing?" | "Nice weather we're having!" | "You're a chatty one!" }
+ [Continue]
-> DONE

- (Ranger)
{ &"I'm busy" | "Don't interrupt me" | "What is it?" }
+ [Continue]
-> DONE

- (Fighter)
{ &"Hey I'm training!" | "Can't stop to talk, getting sick gains!"  }
+ [Continue]
-> DONE

=== second_day
// SECOND DAY *****************************************
// searching for cave entrance
~ nextPathString = "second_day" // will loop in second day

{
    - class == "Rogue":
        -> Rogue_intro
    - class == "Magician":
        -> Magician_intro
    - class == "Ranger":
        -> Ranger_intro
    - class == "Fighter":
        -> Fighter_intro
}

= Rogue_intro
{ second_day.Rogue_intro > 1 : -> post}
"Hello, I'm Rogue {name}. It is nice to have a chance to talk. You were so busy last night. How are you enjoying your inn so far?"
* "A lot[."], it has so much potential. With some work things will really go great."
* "Not much[."], I didn't realize how much work it took to run things."
- "Yea, it is a lot of work, but the potential is there. I can't wait to see what you do with the place."
* "So what are you doing[."] out here anyways?"
* "Thanks[."], and good luck with whatever you're doing out here!"
    -> fin
- "Well aren't you the curious little inn keeper. I don't know if we're good enough friends for me to let you know that. Let's just say I'm looking for something."
- (fin)
+ [Continue]
-> DONE
 

= Magician_intro
{ second_day.Magician_intro > 1 : -> post}
"Hey, I'm Magician {name}. So you're the new owner of the inn. I hope you stock up on tea leaves. I'm know for being thirsty, amongst other things, especially after spending all day out in the sunshine."
* "What are you doing[."] out here anyways?"
* "Sure...[."] I'll get right on that."
    -> fin
- "Shh, that's my business. Plus if I told you I'd have to worry about you poisoning me and trying to steal my boots"
+ ["That wouldn't happen."]
- "Exactally what a theif would say! Maybe once we are better friends I'll let you know. As long as I don't see you eyeing my boots."
- (fin)
+ [Continue]
-> DONE

= Ranger_intro
{ second_day.Ranger_intro > 1 : -> post}
- "I'm Ranger {name}. I guess you're probably wondering what all these people are doing out here anyways?"
* "Funny you should mention that[."], I was just about to ask." 
- "They will play coy like they aren't, but they are looking for the entrance to the cave which is rumored to be around here."
- (cave)
* "Why would they care about a cave?" 
"Supposedly inside there are all sorts of monsters inside which one could fight to prove their bravery." -> cave
* "What like for spelunking?"
"That and the rumors of a wealth of gold and precious gems hidden inside its depths."     -> cave
* "Well I'm not going in any freaky cave."
- "And I don't blame you. Who'd run the inn? Besides you don't look like the type to follow others. I respect that, even if you do seem a little timid."
    + [Continue]
    -> DONE

= Fighter_intro
{ second_day.Fighter_intro > 1 : -> post}
"I'm Fighter {name}. So you decided to take over the inn, very bold of you, I admire your courage."
- (cave)
    * "What are you up to[?"], out here anyways?" 
"You know I've been so busy chasing wealth I never even stopped to ask myself that. What even am I doing out here?"-> cave
    * "You aren't another one[."] of those cave explorors are you?" 
"Amazing, how could you tell? And who told you about the cave it is supposed to be a secret." -> cave
    * "Well I hope you figure out all that[."], even if you seem a little confused I'm sure it will all work out." 
- "You know, even when things look bleak they have a way of working themselves out."
        + [Continue]
        -> DONE

// day 2 post story dialogue
= post
{ day >= 3 : -> third_day } // break out of loop if it is 3nd day
{
        - class == "Rogue":
            -> Rogue
        - class == "Magician":
            -> Magician
        - class == "Ranger":
            -> Ranger
        - class == "Fighter":
            -> Fighter
    }
    
- (Rogue)
{ &"You should get some ingredients for the inn!" | "Cute, my own fan!" | "hmm..." }
+ [Continue]
-> DONE

- (Magician)
{ &"Eyeing my boots?" | "I've got my eyes on you!" | "Oh man am I thirsty!" }
+ [Continue]
-> DONE

- (Ranger)
{ &"If you find the cave entrance we could split the reward!" | "I'm not sneaking, just curious!" | "What about over here? hmm..." }
+ [Continue]
-> DONE

- (Fighter)
{ &"Gosh what am I doing out here?" | "I bet that cave isn't even a thing!" | "If I gain any more muscle I will be absolutely swole!" }
+ [Continue]
-> DONE

=== third_day
// THIRD DAY *****************************************
// adventurers found the cave entrance
~ nextPathString = "third_day"
{
    - class == "Rogue":
        -> Rogue_intro
    - class == "Magician":
        -> Magician_intro
    - class == "Ranger":
        -> Ranger_intro
    - class == "Fighter":
        -> Fighter_intro
}

= Rogue_intro
{ third_day.Rogue_intro > 1 : -> post}
"You will never believe this, but I think I figured out where the cave  entrance is."
+ ["Where?"]
- "On the West face of the cliffs there is a spot where it opens up!"
+ ["Crazy! Are you gonna go inside?]
- "Yes, but don't tell the others I want a head start!"
+ [Continue]
-> DONE


= Magician_intro
{ third_day.Magician_intro > 1 : -> post}
"Hey! I lost my boots"
+ ["This again?"] "Why would I want your stinky old boots?"
- "It is fine consider them my gift to you"
+ ["Great..."]
- "When I was looking for them I discovered an opening in the west face of the cliffs that I think is worth exploring. As soon as I find new kicks I'm going down!"
+ [Continue]
-> DONE

= Ranger_intro
{ third_day.Ranger_intro > 1 : -> post}
"Wow I made the most incredible discovery"
+ ["What is it?"]
- "I was down on the western field and I heard some strange sounds coming from the cliffs. I found what looks to be an entrance going inside to the caves!"
+ ["So are you going to go down in it?"]
- "Tomorrow. First I'll have to gather up some potions and pastries. There is no telling how deep it goes.
+ [Continue]
-> DONE

= Fighter_intro
{ third_day.Fighter_intro > 1 : -> post}
"I spent time reflecting and now I figured out my purpose for being out here"
+ ["So what are you doing out here?"]
- "I realized I never wanted to be a fighter but I just fell into the role society pushed onto me!"
+ ["So what are you going to do now?"]
- "Oh man, I never even though about that... What am I going to do now?"
+ [Continue]
-> DONE

// day 3 post story dialogue
= post
{ day >= 4 : -> fourth_day } // break out of loop if it is 4th day
{
        - class == "Rogue":
            -> Rogue
        - class == "Magician":
            -> Magician
        - class == "Ranger":
            -> Ranger
        - class == "Fighter":
            -> Fighter
    }
    
- (Rogue)
{ &"Hope I find some nice emeralds! Green is my colour!" | "Let's just pretend this never happened!" | "Shh..." }
+ [Continue]
-> DONE

- (Magician)
{ &"First I need a nap" | "I'm getting thirsty" | "What if I get lost inside?" }
+ [Continue]
-> DONE

- (Ranger)
{ &"Need some saucy sweets for my journey underneath" | "What kind of potions are good for decending into the ground?" |"Okay this is it don't get scared now!" }
+ [Continue]
-> DONE

- (Fighter)
{ &"What do I do now?" | "Maybe if I get back to my roots?" | "Maybe I'll do some pushups?"  }
+ [Continue]
-> DONE

=== fourth_day
 // FOURTH DAY *****************************************
// adventurers go inside cave
~ nextPathString = "fourth_day"
{
    - class == "Rogue":
        -> Rogue_intro
    - class == "Magician":
        -> Magician_intro
    - class == "Ranger":
        -> Ranger_intro
    - class == "Fighter":
        -> Fighter_intro
}

= Rogue_intro
{ fourth_day.Rogue_intro > 1 : -> post}
"Today's the day. I'm going to go inside the caves"
+ ["Exciting!"]
- "I just hope I can find the treasure before the others. I've got expensive tastes!"
  + [Continue]
 -> DONE
 

= Magician_intro
{ fourth_day.Magician_intro > 1 : -> post}
"I found some paper bags I can use as shoes. So I'm going inside the caves today."
+ ["Astounding..."]
- "I hope I brought enough potions, I'd hate to be thirsty with nothing to drink"
+ [Continue]
-> DONE

= Ranger_intro
{ fourth_day.Ranger_intro > 1 : -> post}
"I've got a stock of supplies for my trip into the caves"
+ ["Be safe"]
- "As long as I don't get lost I'll be alright"
+ [Continue]
-> DONE

= Fighter_intro
{ fourth_day.Fighter_intro > 1 : -> post}
"I figured out what I'm gonna do now"
+ ["What's that?"]
- "Last night in the inn everyone was talking about exploring the caves so I'm gonna follow them down so they have a strong fighter to help!"
+ ["Follow them..."]
- "Oh no. I'm doing it again just following the crowd."
        + [Continue]
        -> DONE

// day 3 post story dialogue
= post
{ day >= 5 : -> fifth_day } // break out of loop if it is 5th day
{
        - class == "Rogue":
            -> Rogue
        - class == "Magician":
            -> Magician
        - class == "Ranger":
            -> Ranger
        - class == "Fighter":
            -> Fighter
    }
    
- (Rogue)
{ &"What will I buy first? | "Satin or silk you think?" | "That fighter looks like he could carry lots of treasure!" }
+ [Continue]
-> DONE

- (Magician)
{ &"Hope these shoes don't get wet!" | "Maybe I'll have a little pre cave drink!" | "Hey where did my belt go?" }
+ [Continue]
-> DONE

- (Ranger)
{ &"I hope I brought enough supplies" | "I'm gonna need several doughnuts after this is over!" | "It's now or never I guess!" }
+ [Continue]
-> DONE

- (Fighter)
{  &"How can I become independant!" | "I need you to tell me!" | "Tell me what to do!" }
+ [Continue]
-> DONE

=== fifth_day
// FIFTH DAY *****************************************
// adventurers find a cockatrice and cows gaurding treasure
~ nextPathString = "fifth_day"
{
    - class == "Rogue":
        -> Rogue_intro
    - class == "Magician":
        -> Magician_intro
    - class == "Ranger":
        -> Ranger_intro
    - class == "Fighter":
        -> Fighter_intro
}

= Rogue_intro
{ fifth_day.Rogue_intro > 1 : -> post}
"Yesterday inside the caves there was a room in the back which was full of gold and gems. 
+ ["So are you rich now!"]
- "I wish, inside was a Giant Cockatrice and several armed Cows protecting it! "
+ ["Uh what?"]
- "We all got frightened and retreated. We will try going back tomorrow!"
  + [Continue]
 -> DONE
 

= Magician_intro
{ fifth_day.Magician_intro > 1 : -> post}
"Wow I nearly lost my shirt in those caves!"
+ ["How surprising..."]
- "There was some huge bird snake thing and several Cow standing upright armed with weapons"
+ ["Bet they had shoes too..."]
- "If I could find a way to sneak in there I could buy an entire wardrobe"
        + [Continue]
        -> DONE

= Ranger_intro
{ fifth_day.Ranger_intro > 1 : -> post}
"Incredible there is a whole room filled with treasure!"
+ ["So about your tab..."]
- "Even more incredible there is a humongus Cockatrice with it's own malitia of Cow gaurds"
+ ["Explains why your tab is still open..."]
- "Tomorrow we will go inside and challenge them to a contest for their treasure!"
        + [Continue]
        -> DONE

= Fighter_intro
{ fifth_day.Fighter_intro > 1 : -> post}
"I Followed the other into this room in the back of the caves and almost got turned to stone by a Cockatrice's gaze!"
+ ["I thought you were going to do your own thing"]
- "The Rogue said I would be a conformest if I didn't follow the crowd into the caves!"
+ ["You believed them?"]
- "Wait am I suppost to follow them and do, or follow the ones who don't? Now I'm confused"
        + [Continue]
        -> DONE

// day 5 post story dialogue
= post
{ day >= 6 : -> sixth_day } // break out of loop if it is 6th day
{
        - class == "Rogue":
            -> Rogue
        - class == "Magician":
            -> Magician
        - class == "Ranger":
            -> Ranger
        - class == "Fighter":
            -> Fighter
    }
    
- (Rogue)
{  &"So much money!" | "Maybe I can convince the Magician to be a distraction!" | "Too much to carry myself anyways!" }
+ [Continue]
-> DONE

- (Magician)
{  &"Such majectic creatures!" | "I bet they are thirst too!" | "I wounder if I can convince them to trade me for pastries!" }
+ [Continue]
-> DONE

- (Ranger)
{  &"Hey can you front me some potions?" | "Better throw in some pastries!" | "I'm good for it!" }
+ [Continue]
-> DONE

- (Fighter)
{  &"Could you just tell me what to do?" | "It's hard making desicions" | "I think I'll just go along with whatever they are doing!"  }
+ [Continue]
-> DONE

=== sixth_day
// SIXTH DAY *****************************************
// adventurers defeat monster and get treasure END OF STORY
~ nextPathString = "sixth_day"
{
    - class == "Rogue":
        -> Rogue_intro
    - class == "Magician":
        -> Magician_intro
    - class == "Ranger":
        -> Ranger_intro
    - class == "Fighter":
        -> Fighter_intro
}

= Rogue_intro
{ sixth_day.Rogue_intro > 1 : -> post}
"We went back to get the treasure out of the Cave!"
+ ["Combat?"]
- "No it turns out the Cockatrice and cows are a theater troup and they just wanted an audience to perform for!"
+ ["So how did they end up with a room of treasure then?"]
- "It turns out it is just an elaborate set for the play their rehersing!"
  + [Continue]
 -> DONE
 

= Magician_intro
{ sixth_day.Magician_intro > 1 : -> post}
"That is it no more potions for me!"
+ ["Why not?"]
- "I just sat through a play with talking cows and a giant cockatrice! I'm thinking of getting myself checked out."
+ ["Well I'm not convinced you're not insane!"]
- "Thanks! Wait, what do you mean..."
        + [Continue]
        -> DONE

= Ranger_intro
{ sixth_day.Ranger_intro > 1 : -> post}
"I thought I was in for a fight but instead I had to sit throught the most boring play!"
+ ["So about the money you owe me?"]
- "And the worst part is they passed around a hat after and I was obliged to donate the rest of my money!"
+ ["I guess you can clean dishes or something at the inn..."]
- "I've got a new plan to make a fortune selling self help books!"
        + [Continue]
        -> DONE

= Fighter_intro
{ sixth_day.Fighter_intro > 1 : -> post}
"I'm no longer confused about my lives meaning!"
+ ["Did you stop following the crowd?"]
- "I found my true calling as a thespian!"
+ ["What?"]
- "I joined the theater troup. Now I'll have a director to tell me what to do and say..."
        + [Continue]
        -> DONE

// day 6 post story dialogue
= post
// { day >= 6 : -> sixth_day } // never break out of loop since story ends
{
        - class == "Rogue":
            -> Rogue
        - class == "Magician":
            -> Magician
        - class == "Ranger":
            -> Ranger
        - class == "Fighter":
            -> Fighter
    }
    
- (Rogue)
{  &"Guess it is back to making a dishonest living as a Rogue!" | "That one bovine sure can act!" | "I'm not sure a cave is a great place to stage a play!" }
+ [Continue]
-> DONE

- (Magician)
{  &"Best thing is you get a free gown at the facility!" | "Won't need shoes if I don't leave my room!" | "I'm gonna miss you!" }
+ [Continue]
-> DONE

- (Ranger)
{  &"How to make 1 million dollars." | "I'm qualified to write this book!" | "I'd be a millionare if circumstances were different." }
+ [Continue]
-> DONE

- (Fighter)
{  &"What are my lines?" | "What should I wear? Better ask wardrobe" | "If I perform good Director says I can pick what I eat tonight?"  }
+ [Continue]
-> DONE

 -> END