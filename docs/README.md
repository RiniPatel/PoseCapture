### Christopher Hung (christoh), Rini Patel (rinip), and Tushar Goyal (tgoyal1)

## Introduction
We implemented a wireless system that captures a person’s movements as they move their limbs. The system uses a network of nodes each with an IMU which relay the orientation measurements for the respective limbs to a master on a PC, which processes the data and represents the estimated pose in form of graphical 3D model.

![Stick Figures](http://vis.uky.edu/~gravity/Research/Mocap/Mocap_files/image002.jpg)

## Motivation 
The motivation behind this project is the cases when we require mobility, flexibility and line of sight requirement is not necessary. We took a wireless approach with inertial measurement sensors (IMUs), where the sensors could be attached to the body and the receiver software running on the host would use the inertial data to estimate the pose of the subject.

## Key Use Cases
- Computer Animation
- Virtual Reality
- Film industry
- Gaming
- Robotics 

## System Design 
Key hardware components for our project:
- Arduino Uno boards + WiFi shields
- IMU Sensors

![Block Diagram](https://github.com/RiniPatel/PoseCapture/tree/master/docs/assets/Pose_Capture.png)


## References

1. [Using Inertial Sensors for Position and Orientation Estimation](https://arxiv.org/pdf/1704.06053.pdf)
2. [Inertial Motion Capture Costume Design Study](https://www.ncbi.nlm.nih.gov/pmc/articles/PMC5375898/)

