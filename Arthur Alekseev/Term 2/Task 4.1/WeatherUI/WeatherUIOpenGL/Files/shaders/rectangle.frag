#version 330
out vec4 outputColor;
in vec2 texCoord;
uniform vec2 innerResolution;

uniform sampler2D texture0;
uniform vec4 color;

void main()
{
    float corners = 3.0;
    float x = texCoord.x * innerResolution.x;
    float y = texCoord.y * innerResolution.y; 
	
    outputColor = color - vec4(vec3(0.0), texCoord.y * 0.22) - vec4(vec3(0.0), texCoord.x * 0.2) - vec4(vec3(0.0), 0.22);

    float corner_x = min(x, innerResolution.x - x);
    float corner_y = min(y, innerResolution.y - y);

    if(sqrt(corner_x) + sqrt(corner_y) < corners){
        
        outputColor = vec4(0.0);
    }
}