#version 330 core
out vec4 FragColor;
in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoord;

uniform sampler2D texture1;
uniform sampler2D texture2;
uniform vec3 objectColor;
uniform vec3 lightPos;
uniform vec3 viewPos;

struct Material {
    vec3 ambient;       // 环境：环境光照下表面反射的是什么颜色，通常与表面颜色相同
    vec3 diffuse;       // 漫反射：期望的物体颜色
    vec3 specular;      // 镜面：物体表面上的镜面高光的颜色
    float shininess;    // 反光度：镜面高光的散射/半径
};

uniform Material material;

struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform Light light;

void main()
{
    // 环境光
    vec3 ambient = light.ambient * material.ambient;

    // 漫反射 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * (diff * material.diffuse);

    // 镜面光
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * material.specular);  

    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
}
