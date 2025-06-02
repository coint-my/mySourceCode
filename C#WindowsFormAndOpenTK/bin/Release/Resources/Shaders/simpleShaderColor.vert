#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 view;
uniform mat4 model;
uniform mat4 projection;

void main()
{
    mat4 modelView = model * view;

    modelView[0][0] = model[0][0];
    modelView[0][1] = 0.0;
    modelView[0][2] = 0.0;
    
    modelView[1][0] = 0.0;
    modelView[1][1] = model[1][1];
    modelView[1][2] = 0.0;
    
    modelView[2][0] = 0.0;
    modelView[2][1] = 0.0;
    modelView[2][2] = model[2][2];

    gl_Position = vec4(aPosition, 1.0) * modelView * projection;
}