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
- Real-time AI feedback
- Collaboration on the same AR environment
- Tool recognition using computer vision
- IOS ARKit development
- Classroom / lab safety practice

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

# Many challenges still to come
![Screenshot 2023-10-30 140037](https://github.com/TungVietLe/HandsOn/assets/99946449/d3d547d4-d10c-4dba-bd61-728abf012ea6)
It's just a super early state. The model is still improving and hasn't generalized things well enough. Currently, the project is just a prototype with some simple requests. 

# Future goals
1. AI Real-time feedback\
a. Anticipated use case:\
i. “Why is my spectrometer not giving accurate readings?”\
- Real-Time Feedback: Hands On can suggest checking the alignment,
the quality of the light source, or the calibration of the spectrometer.\
ii. "Is it safe to heat this substance with the Bunsen burner?"\
- Real-Time Feedback: Hands On can provide safety guidelines,
including the substance's properties and suitable temperature ranges
for heating.\

2. AR Multi-user in the same environment / Tools recognition
using computer vision
- Enhance collaboration that allows students to work together and share
experiences.
- Allow teachers to demonstrate how the experiment should be conducted.
- Import tools into the simulation.
