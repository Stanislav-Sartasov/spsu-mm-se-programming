// modified from the Art of code tutorial by BIgWings https://youtu.be/xDxAnguEOn8
#version 330

#define NUM_EXPLOSIONS 5.
#define NUM_PARTICLES 50.

uniform vec2 iResolution;
uniform float iTime;

out vec4 outputColor;
in vec2 texCoord;

vec2 Hash12(float t) {
    float x = fract(sin(t*354.3)*234.2);
    float y = fract(sin((t+x)*137.3)*159.2);
    return vec2(x,y);
}
vec2 Hash12Polar(float t) {
    float a = fract(sin(t*354.3)*234.2)*6.2832;
    float d = fract(sin((t+a)*137.3)*263.2);
    return vec2(sin(a), cos(a))*d;
}
float Explosion(vec2 uv, float t, float id){
#ifndef NUM_PARTICLES
    float NUM_PARTICLES = 5.+70.*Hash12(id).x;
#endif
    float col=0.;
    for (float i=0.; i < NUM_PARTICLES; i++){
        float rnd = Hash12(id+i).x;
        vec2 dir = Hash12Polar(i+1.+id+NUM_PARTICLES)*.5;
        dir *= rnd;
        float y = 0.02*t*t+t*t*0.3*(rnd)-(sin(t)*.5+0.5)*0.5*id+0.3;
        float d = length(uv-dir*(smoothstep(-0.5,1.,t)-0.2)+vec2(0,y));

        float brightness = mix(.0005, .001, smoothstep(.05,0., t));
        
        //brightness += 0.0002*id;
        brightness *= (sin(t*(15.+id*29.)+i)*.5+.5)*rnd+0.1*rnd;
        brightness *= smoothstep(1., .15, t);
        
        col += brightness/d;   
        //col = 0.01/d;
    }
    return col;
}
void main()
{
    vec2 uv = texCoord;
        
    float time = iTime/2.+44.;
    //float NUM_EXPLOSIONS = (sin(iTime)*0.5+0.5)*5.;
    
    vec3 col = abs(vec3(1.-abs(dot(uv,uv)))*0.05)*vec3(0.01,0.01,1.); //bg
    for (float i=0.; i < NUM_EXPLOSIONS; i++){
        float t = time+i/NUM_EXPLOSIONS;
        float ft = floor(t);
        vec3 color = sin(3.37*vec3(.34,.65,32)*ft)*.3+.7;
        vec2 offs = Hash12(i+1.+ft*NUM_EXPLOSIONS)-0.5;
        offs *= vec2(iResolution.x/iResolution.y,1.);
        //col += .00041/length(uv-offs);
        float rnd = Hash12(i+1.+color.y*20.+color.x*20.+color.z*20.).x+0.5;
        col += Explosion(uv-offs,fract(t), rnd)*color;
    }
    col *= 2.;
        //col = vec3(Hash12(uv.x*uv.y),0.);

    outputColor = vec4(col,1.0);
}