# Hands On AI Argumented Lab Demo
Laboratory equipment are expensive. Learning on 2D blackboard is unengaging. Hands On is AR simulation equipped with AI for both teachers & students to interact, experiment, and make mistakes freely.

![Screenshot 2023-10-30 094619](https://github.com/TungVietLe/HandsOn/assets/99946449/417254ae-a0df-4f6d-afbb-72be4eff66b2)


# Video
- Demo Use: https://youtu.be/0ZqBgqMGOlc?si=09HIryc_1vLpYP-l
- Demo Notes Drawing: https://youtu.be/-qrMq-FZTkg?si=WSMF8oVQxwj4frcs

# Interacting with the environment using natural language
a. Speak to Hands On:
- “I need a weight[object] made of iron[material] to conduct a buoyancy
force experiment”.
- “Change the simulation gravity[field] to the moon’s gravity[value]”.
- “Set the height[field] of liquid[target] to 57cm[value]”.
- “Spawn something to measure length[description]”.

b. Best Practices:
i. It’s good to keep it concise about what your intentions are.
ii. You can ask Hands On to assist you with details. Example: “Show me
the forces analysis of objects.”
iii. Beside speech interaction, you can also interact with the simulation
using real-world physical movement. Example: “grab object, move
object around.”
iv. Users are also able to take notes anywhere in the environment, so the
world is your infinite, limitless canvas.

# What's next for Hands On
Real-time feedback
Collaboration on the same AR environment
Tool recognition using computer vision
IOS ARKit development

# Inspiration
I remember the first time I went to my physics lab during my high school years to experiment with electronic circuits. The equipment looks nothing familiar to me even though I have been studying it for a year. Turn out I was working with 2D symbol drawings the whole time, without touching or experiencing myself.

# What it does
Laboratory equipment are expensive. Learning on the 2D blackboard is unengaging. Making Physic Experiment accessible anywhere, Hands On is an AR simulation for both teachers and students to interact, experiment, and make mistakes freely. It makes classroom experience more engaging for everyone. Better yet, Hands On is equipped with AI to instruct, explain to students in real-time: like "You should turn off the power source before plugging in", or perform actions like "Give me the set of equipment for Archimedes experiment", which can enhance creativity, safety, and familiarity.


![Screenshot 2023-10-30 094740](https://github.com/TungVietLe/HandsOn/assets/99946449/7bd3e593-048c-4d37-b5f2-1783798e9a94)

# How we built it
Input utterances, label data, train, test, improve, and deploy AI model using Azure CLU.
Connected Cognitive Search and SQL database.
Code 3D physic simulations from scratch using Unity C#
Develop for Android AR Core

