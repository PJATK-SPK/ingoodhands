module "ingoodhands" {
  source = "../../modules/ingoodhands"

  environment = "dev"
  google-project = var.google-project
}
