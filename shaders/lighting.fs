#version 330 core
out vec4 FragColor;
in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoord;

uniform vec3 objectColor;
uniform vec3 viewPos;

struct Material {

    // vec3 ambient;         移除了环境光颜色，因为环境光约等于漫反射光,当然也可以保留环境光
    sampler2D diffuse;      // 漫反射贴图，Opaque Type，无法在着色器中被实例化
    sampler2D specular;     // 镜面光贴图，采集贴图亮度
    float shininess;        // 反光度：镜面高光的散射/半径
    sampler2D emission;     // 放射光贴图，属于物体的自发光
};

uniform Material material;

struct Light {
    vec3 position;      // 位置

    vec3 ambient;       // 环境
    vec3 diffuse;       // 漫反射
    vec3 specular;      // 镜面
};

uniform Light light;

void main()
{
    // 环境光
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoord).rgb);

    // 漫反射 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * (diff * vec3(texture(material.diffuse, TexCoord)).rgb);

    // 镜面光
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * (vec3(texture(material.specular, TexCoord))).rgb);  

    vec3 emission = vec3(texture(material.emission, TexCoord)).rgb;

    vec3 result = ambient + diffuse + specular + emission;
    FragColor = vec4(result, 1.0);


}
