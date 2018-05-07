### Christopher Hung (christoh), Rini Patel (rinip), and Tushar Goyal (tgoyal1)

## Introduction
We implemented a wireless system that captures a person’s movements as they move their limbs. The system uses a network of nodes each with an IMU which relay the orientation measurements for the respective limbs to a master on a PC, which processes the data and represents the estimated pose in form of graphical 3D model.

<p align="center"> 
<img src="http://vis.uky.edu/~gravity/Research/Mocap/Mocap_files/image002.jpg" width="375" height="125">
</p>

## Motivation
Following are the popular approaches to do pose capture:
- Wired sensors - become tangled and difficult to move around in
- Mechanical frames with sensors - intrusive and non-portable
- Computer vision - illumination differences and obstructions limit accuracy

The motivation behind this project is the cases when we require mobility, flexibility and line of sight requirement is not necessary. We took a wireless approach with inertial measurement sensors (IMUs), where the sensors could be attached to the body and the receiver software running on the host would use the inertial data to estimate the pose of the subject.

## Project Goals
The main goal of the project is to get a full body capture of the person using 
1. wireless sensor network
2. robust under variety of environmental conditions, and 
3. use the orientation data from IMUS to post process in the form of 3D body model.

## Key Use Cases
The most definitive use of our project is in computer animation. Rather than manually moving the limbs of a character, the animator can build a model of the character, act out the desired motion physically, and apply the recorded motions directly to the limbs of the model. This animation could be used in film making, game development etc.
<p align="center"> 
<img src=assets/usecases.png>
</p>

<p align="center"> 
<img src=assets/usecases.png>
</p>


## System Design 
Key hardware components for our project:
- IMU Sensors (SparkFun 9DoF Razor IMU M0)
- Batteries for IMUs (Li Ion Batteries (3.7V 400mAh))
- WiFi shields (ESP8266 WiFi modules)


<p align="center"> 
<img src=assets/Pose_Capture.png width="460" height="300">
</p>

<p align="center"> 
<img src=assets/hardware.png>
</p>

## Body Model
We are using a hip rooted body model where all the movements of bones are rooted on a tree as shown in Figure 3. This defines the relative position of bones and helps in modelling body motion and movements correctly. For example, upper arm  is parent of lower arm thus a movement in upper arm would cause motion in lower arm as well. The same model is being used by Blender and Unity.
<p align="center"> 
<img src=assets/body_model.png>
</p>

<p align="center"> 
<img src=assets/body_model1.png>
</p>

## Project Status
<p align="center"> 
<img src=assets/project_status.png>
</p>

## Conclusion
<p align="center"> 
<img src=assets/conclusion.png>
</p>

## References

1. [Using Inertial Sensors for Position and Orientation Estimation](https://arxiv.org/pdf/1704.06053.pdf)
2. [Inertial Motion Capture Costume Design Study](https://www.ncbi.nlm.nih.gov/pmc/articles/PMC5375898/)
