SERVICES = ['Criterion', 'AdGroup', 'Campaign', 'TrafficEstimator', 'Report',
  'Info', 'Account', 'KeywordTool', 'Ad', 'SiteSuggestion']
version = "v11"
prefix = "https://adwords.google.com/api/adwords/" + version + "/"
srcdir = "src" + version

# The default task is run if rake is given no explicit arguments.

desc "generates client csharp files based on wsdl urls"
task :default => [:genclient]

desc "generates client csharp files based on wsdl urls"
task :genclient do
  SERVICES.each do |s|
      command = "wsdl /namespace:com.google.api.adwords." + version + " " + prefix + s + "Service?wsdl"
      system(command)
    end
end
