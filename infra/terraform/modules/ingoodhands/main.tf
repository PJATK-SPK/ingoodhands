module "instance" {
  source = "./modules/instance"

  environment = var.environment
  google-project = var.google-project
}

module "basics" {
  source = "./modules/basics"

  google-project = var.google-project
}
